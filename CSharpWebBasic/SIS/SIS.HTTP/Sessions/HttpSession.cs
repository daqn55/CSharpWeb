using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> Sessions;

        public HttpSession(string id)
        {
            this.Id = id;
            this.Sessions = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            this.Sessions.Add(name, parameter);
        }

        public void ClearParameters()
        {
            this.Sessions.Clear();
        }

        public bool ContainsParameter(string name)
        {
            if (this.Sessions.ContainsKey(name))
            {
                return true;
            }

            return false;
        }

        public object GetParameter(string name)
        {
            var parameter = this.Sessions[name];

            if (parameter != null)
            {
                return parameter;
            }

            return null;
        }
    }
}
