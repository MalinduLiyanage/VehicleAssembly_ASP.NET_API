namespace Vehicle_Assembly.Utilities.AccountUtility.AdminAccount
{
    public interface IAdminAccountUtility
    {
        public bool VerifyPassword(string password, byte[] dbPassword);
    }
}
