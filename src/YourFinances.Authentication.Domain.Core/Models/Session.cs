using System;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class Session
    {
        public Session(int userId, string refreshToken, DateTime expirationDate, DateTime createDate, int sessionId, bool active)
        {
            UserId = userId;
            RefreshToken = refreshToken;
            ExpirationDate = expirationDate;
            CreateDate = createDate;
            SessionId = sessionId;
            Active = active;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int SessionId { get; private set; }
        public bool Active { get; private set; }
    }
}
