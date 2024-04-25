using halalpizzabackend.Models;

public interface IUserDetailsService
{
    UserDetails Authenticate(string username, string password);
    UserDetails AddUser(UserDetails userDetails);
    void DeleteUser(int userId);
    UserDetails EditUser(int userId, UserDetails userDetails);

    UserDetails EditUserWithNewPassword(int userId, UserDetailsEditModel userDetails);

    UserDetails EditMeWithNewPassword(int userId, UserDetailsEditModel userDetails);
    UserDetails GetUserById(int userId);

    IEnumerable<UserDetails> GetAllUsers();
}
