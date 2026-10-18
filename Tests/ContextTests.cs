using Xunit;

namespace AlfaHackaton.Context.Tests;

public class ContextTests
{
    [Fact]
    public void New_PreservesRequestMetadata_WithoutParsingJwt()
    {
        // Контракт переноса: контекст хранит готовый заголовок, не декодирует JWT и сохраняет отмену.
        using var cancellation = new CancellationTokenSource();

        var ctx = Context.New("Bearer synthetic-token", "Hackathon/1.0", "ru", cancellation.Token);

        Assert.Equal("Bearer synthetic-token", ctx.AuthToken);
        Assert.Equal("Hackathon/1.0", ctx.UserAgent);
        Assert.Equal("ru", ctx.Locale);
        Assert.Equal(cancellation.Token, ctx.CancellationToken);
        Assert.Empty(ctx.Origin);
        Assert.Empty(ctx.Authorize.UserId);
    }

    [Fact]
    public void Empty_DoesNotShareHeadersBetweenRequests()
    {
        // Инвариант: заголовок одного запроса не должен попасть в другой контекст.
        var first = Context.Empty();
        first.AdditionalHeaders.Add("X-Request-Id", "first");

        var second = Context.Empty();

        Assert.Empty(second.AdditionalHeaders);
        Assert.NotSame(first.Authorize, second.Authorize);
    }

    [Fact]
    public void EmptyWithOrigin_PreservesOrigin_WithoutAuthorization()
    {
        var ctx = Context.EmptyWithOrigin("https://example.com");

        Assert.Equal("https://example.com", ctx.Origin);
        Assert.Empty(ctx.AuthToken);
    }
}
