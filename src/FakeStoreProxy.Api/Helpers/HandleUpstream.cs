using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxy.Api.Helpers;

public static class HandleUpstreamExtensions
{
    public static ActionResult HandleUpstream(this ControllerBase controller, Exception ex)
    {
        return ex switch
        {
            TaskCanceledException => controller.Problem(
                detail: "Timed out while contacting upstream provider.",
                statusCode: StatusCodes.Status504GatewayTimeout),

            HttpRequestException hre => controller.Problem(
                detail: hre.Message,
                statusCode: StatusCodes.Status502BadGateway),

            _ => throw ex
        };
    }
}
