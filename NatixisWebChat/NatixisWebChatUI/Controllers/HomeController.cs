namespace NatixisWebChatUI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NatixisWebChatDomain.AppConstants;
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using NatixisWebChatInfrastructure.Services;
    using NatixisWebChatModels.AppModels;
    using NatixisWebChatUI.ViewsModel;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PasswordServices _passwordServices;
        private readonly AuthenticationServices _authenticationServices;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        /// <summary>The constructor</summary>
        /// <param name="logger">The logger</param>
        /// <param name="usersRepository">The user repository</param>
        /// <param name="passwordServices">The password services</param>
        /// <param name="authenticationServices">The authentication services</param>
        /// <param name="map">The mapper</param>
        public HomeController(ILogger<HomeController> logger, IUsersRepository usersRepository, PasswordServices passwordServices, AuthenticationServices authenticationServices, IMapper map)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _passwordServices = passwordServices;
            _authenticationServices = authenticationServices;
            _mapper = map;
        }

        #region INDEX VIEW

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.WebChatController);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsersModel userModel)
        {
            // check if username and password aren't empty
            if (userModel.Username == null || userModel.Password == null)
            {
                return View(userModel);
            }

            // check if user is valid
            UserEntity? mappedUser = _mapper.Map<UsersModel, UserEntity>(userModel);
            UserEntity? validUser = _authenticationServices.GetValidUser(mappedUser);
            
            if (validUser == null)
            {
                ViewBag.error = "Invalid Account";
                return View(userModel);
            }

            //authenticate user
            await _authenticationServices.CreateAuthentication(validUser, HttpContext);
            return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.WebChatController);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // logging out user(removing cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.HomeController);
        }

        #endregion

        #region REGISTER VIEW

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.WebChatController);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UsersModel userModel)
        {
            if (ModelState.IsValid)
            {
                //check if user with username exists
                var mappedUser = _mapper.Map<UsersModel, UserEntity>(userModel);
                var foundUserByUsername = _usersRepository.GetUserByUsername(mappedUser.Username);
                if (foundUserByUsername != null)
                {
                    ViewBag.error = "User with this name already exists";
                    return View(userModel);
                }
                else
                {
                    //adding new user to db
                    var newUser = _mapper.Map<UsersModel, UserEntity>(userModel);
                    _usersRepository.AddUser(newUser);
                    ModelState.Clear();
                    TempData["SuccessfulRegister"] = "Person successfully created";
                    return RedirectToAction(ActionsConsts.IndexAction);
                }
            }

            if (!ModelState.IsValid)
            {
                return Register();
            }

            return LocalRedirect("~/Home/Index");
        }

        #endregion

        #region FORGET PASSWORD VIEW

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(UsersModel userModel)
        {
            // check if username and email aren't empty
            if (userModel.Username == null || userModel.Email == null)
            {
                ViewBag.error = "Required parameters are missing";
                return View(userModel);
            }

            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validateEmailRegex.IsMatch(userModel.Email))
            {
                ViewBag.error = "Email is invalid";
                return View(userModel);
            };

            // check if user is valid
            var mappedUser = _mapper.Map<UsersModel, UserEntity>(userModel);
            var newRandomPassword = _passwordServices.GenerateNewPassword();
            var validUser = _passwordServices.UpdateUserPasswordByEmail(mappedUser, newRandomPassword);
            if (validUser == null)
            {
                ViewBag.error = "User not found";
                return View(userModel);
            }

            //// send new password email
            //_mailService.SendEmail(validUser, newRandomPassword);
            //TempData["SuccessfulUpdate"] = "Email With New Password Sent";

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(ActionsConsts.LogoutAction, ControllersConsts.HomeController);
            }
            return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.HomeController);
        }

        #endregion

        #region PROFILE VIEW

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var user = _authenticationServices.GetUserByClaim(User);
            if (user == null)
            {
                ViewBag.error = "Please Logout. Account Error.";
                return RedirectToAction(ActionsConsts.IndexAction, ControllersConsts.WebChatController);
            }
            var userModel = _mapper.Map<UserEntity, UsersModel>(user);
            return View(userModel);
        }

        #endregion

        #region RESET PASSWORD VIEW

        [HttpGet]
        [Authorize]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ResetPassword(string oldPswd, string new1Pswd, string new2Pswd)
        {
            // check if fields aren't empty
            if (oldPswd == null || new1Pswd == null || new2Pswd == null)
            {
                ViewBag.error = "Required parameters are missing";
                return View();
            }

            // check if new password is secure
            if (!_passwordServices.IsSecurePassword(new1Pswd))
            {
                ViewBag.error = "New password does not meet requirements!";
                return View();
            }

            // check if passwords are the same
            if (new1Pswd != new2Pswd)
            {
                ViewBag.error = "Passwords do not match!";
                return View();
            }

            // check if old password and new are two different
            if (oldPswd == new1Pswd)
            {
                ViewBag.error = "New and old passwords are similar";
                return View();
            }

            // get current user by claim
            UserEntity? user = _authenticationServices.GetUserByClaim(User);
            if (user == null)
            {
                ViewBag.error = "Please Logout. Account Error.";
                return View();
            }

            //// check if old password match user password
            //if (!BCrypt.Net.BCrypt.Verify(oldPswd, user.Password))
            //{
            //    ViewBag.error = "Wrong Password. Don't remember password? Choose Forget Password option.";
            //    return View();
            //}

            // update password and redirect
            _passwordServices.UpdateUserPassword(user, new1Pswd);
            TempData["ResetPassword"] = "Successful Password Change";
            return RedirectToAction(ActionsConsts.ProfileAction, ControllersConsts.HomeController);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}