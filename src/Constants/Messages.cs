using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Constants
{
    public static class Messages
    {
        public static string USER_NOT_EXISTS = "The specified user doesn't exist.Please try again with the correct credentials";
        public static string USER_ALREADY_EXISTS = "User already exists, please try different credentials";
        public static string USER_INVALID = "User is invalid.";
        public static string USER_EXISTS = "User exists.";
        public static string USER_NOT_ADMIN = "User does not have administrator priviliges.";
        public static string EMAIL_NOT_CONFIRMED = "Email has not yet been confirmed, please confirm your email and try again.";
        public static string INVALID_USERNAME_PASSWORD = "Email or password is invalid, please try again.";
        public static string GENERATE_TOKEN_FAILED = "could not generate access token , please try again.";
        public static string USER_REGISTER_SUCCESS = "user successfully registered.";
        public static string USER_NOT_FOUND = "specified user could not be found.";
        public static string CLAIM_ALREADY_EXIST = "specified claim is already assigned to user, try different value";
        public static string CLAIM_ADDED = "claim successfully assigned to user.";
        public static string CLAIM_ADDING_FAILED = "failed to add claim";
        public static string USER_CLAIM_NOT_ASSIGNED = "user does not have the specified claim assigned.";
        public static string CLAIM_REMOVE_SUCCESS = "claim removed successfully.";
        public static string CLAIM_REMOVE_FAILED = "failed to remove claim";
        public static string PASSWORD_CHANGE_SUCCESS = "password changed successfully.";
        public static string PASSWORD_CHANGE_FAILED = "failed to change password";
        public static string GENERATE_EMAIL_CODE_FAILED = "could not generate email verification code.";
        public static string GENERATE_EMAIL_CODE_SUCCESS = "email verification code generated successfully.";
        public static string PASSWORD_RESET_SUCCEESS = "password was reset successfully.";
        public static string PASSWORD_RESET_FAILED = "password reset failed.";
        public static string EMAIL_CONFIRM_SUCCESS = "email confirmed successfully.";
        public static string EMAIL_CONFIRM_FAILED = "email confirmation failed.";

        public static string INVALID_LOGIN_REQUEST = "Invalid Login Request.";

    }
}
