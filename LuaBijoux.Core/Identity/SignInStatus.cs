﻿namespace LuaBijoux.Core.Identity
{
    public enum SignInStatus
    {
        Success,
        LockedOut,
        RequiresTwoFactorAuthentication,
        Failure
    }
}