namespace DatabaseMigrationSystem.Common.Dto
{
    /// <summary>
    /// Ответ об авторизации
    /// </summary>
    public class AuthenticateInfo
    {
        /// <summary>
        /// id Пользователя.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string JwtToken { get; set; }


        public string RefreshToken { get; set; }
    }
}