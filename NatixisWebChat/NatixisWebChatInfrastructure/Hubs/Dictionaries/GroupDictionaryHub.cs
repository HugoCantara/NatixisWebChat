namespace NatixisWebChatInfrastructure.Hubs.Dictionaries
{
    using NatixisWebChatDomain.AppHubs;

    /// <summary>Class GroupDictionaryHub</summary>
    public class GroupDictionaryHub
    {
        /// <summary>User hub dictionary</summary>
        public Dictionary<string, List<UserHub>> dictionary = new Dictionary<string, List<UserHub>>();

        /// <summary>Add data to dictionary</summary>
        /// <param name="key">The key.</param>
        /// <param name="user">The user.</param>
        public void Add(string key, UserHub user)
        {
            if (this.dictionary.ContainsKey(key))
            {
                List<UserHub> list = this.dictionary[key];
                if (!list.Contains(user))
                {
                    list.Add(user);
                }
            }
            else
            {
                var list = new List<UserHub> { user };
                this.dictionary.Add(key, list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Remove(string key, UserHub value)
        {
            if (this.dictionary.ContainsKey(key))
            {
                List<UserHub> list = this.dictionary[key];
                var user = list.Find(x => x.Name == value.Name);
                if (user != null)
                {
                    list.Remove(user);
                }
                if (list.Count == 0)
                {
                    dictionary.Remove(key);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<UserHub>? GetListOfGroupUsers(string key)
        {
            if (this.dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string>? GetListOfGroups()
        {
            return dictionary.Keys.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void RemoveUserFromGroup(UserHub value)
        {
            var listOfGroups = dictionary.Keys.ToList();
            foreach (var group in listOfGroups)
            {
                if (this.dictionary.ContainsKey(group))
                {
                    List<UserHub> list = this.dictionary[group];
                    var user = list.Find(x => x.Name == value.Name);
                    if (user != null)
                    {
                        list.Remove(user);
                    }
                    if (list.Count == 0)
                    {
                        dictionary.Remove(group);
                    }
                }
            }

        }
    }
}
