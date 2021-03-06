﻿namespace HAA.BusinessEntities
{
    public enum UserType { User = 0, Admin = 1, SuperUser = 2 }

    public class UserEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public string UserLevel { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
