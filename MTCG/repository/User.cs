using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository
{
    public class User
    {
        private String username {get; set;}
        private String name { get; set; }
        private String password { get; set; }
        private String token { get; set; }
        private String bio { get; set; }
        private String image { get; set; }
        
        public User(String username, String name ,String password, String token, String bio, String image)
        {
            this.username = username;
            this.name = name;
            this.password = password;
            this.token = token;
            this.bio = bio;
            this.image = image;
        }

        public String toStringUser()
        {
            return "User{" + "username: " + this.username + " password: " + this.password + "}";
        }
    }
}
