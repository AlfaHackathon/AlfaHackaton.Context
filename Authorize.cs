namespace AlfaHackaton.Context;

public class Authorize
{
    private Authorize()
    {
    }

    public Authorize(string userId, string userName, string email)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
    }

    public string UserId { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    public static Authorize Empty()
    {
        return new Authorize();
    }
}
