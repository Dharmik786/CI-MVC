﻿namespace CI_Platform.Models
{
    public class ResetPassword
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
