namespace AlfaHackaton.Context;

public class Context
{
    private Context()
    {
    }

    public Context(
        string authToken,
        string locale,
        string origin,
        string userAgent,
        Authorize authorize,
        CancellationToken cancellationToken,
        Dictionary<string, string>? additionalHeaders = null)
    {
        AuthToken = authToken;
        Locale = locale;
        Origin = origin;
        UserAgent = userAgent;
        Authorize = authorize;
        CancellationToken = cancellationToken;
        AdditionalHeaders = additionalHeaders ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    public string AuthToken { get; private set; } = string.Empty;
    public string Locale { get; private set; } = string.Empty;
    public string Origin { get; private set; } = string.Empty;
    public string UserAgent { get; private set; } = string.Empty;
    public Authorize Authorize { get; private set; } = Authorize.Empty();
    public CancellationToken CancellationToken { get; private set; }
    public Dictionary<string, string> AdditionalHeaders { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public static Context New(string token, CancellationToken cancellationToken = default)
    {
        return New(token, string.Empty, string.Empty, cancellationToken);
    }

    public static Context New(string token, string userAgent, string locale, CancellationToken cancellationToken = default)
    {
        return new Context(token, locale, string.Empty, userAgent, Authorize.Empty(), cancellationToken);
    }

    public static Context Empty()
    {
        return new Context();
    }

    public static Context EmptyWithOrigin(string origin)
    {
        return new Context { Origin = origin };
    }
}
