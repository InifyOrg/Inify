﻿using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure
{
    public interface IUsersDataLayer
    {
        Task<AccessToken> AddAccessToken(AccessToken newAccessToken, long userId);
        Task<User> AddUser(User newuser);
        Task<bool> Edit(User userById);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(long id);
    }
}