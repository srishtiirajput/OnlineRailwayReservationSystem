using Microsoft.AspNetCore.Mvc;

namespace RailwayReservation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public GatewayController(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    [HttpPost("ProcessPayment")]
    public async Task<IActionResult> ProcessPayment()
    {
        var client = _clientFactory.CreateClient();
        var paymentUrl = _configuration["ServiceUrls:PaymentMicroService"];
        var response = await client.GetAsync($"{paymentUrl}/api/Payment/ProcessPayment");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }

    [HttpPost("CompletePayment")]
    public async Task<IActionResult> CompletePayment()
    {
        var client = _clientFactory.CreateClient();
        var paymentUrl = _configuration["ServiceUrls:PaymentMicroService"];
        var response = await client.GetAsync($"{paymentUrl}/api/Payment/CompletePayment");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }


}
