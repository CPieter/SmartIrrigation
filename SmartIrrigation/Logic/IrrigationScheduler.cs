using SmartIrrigation.MQTT;

namespace SmartIrrigation.Logic;

public class IrrigationScheduler
{
    private readonly IrrigationAlgorithm _algorithm;
    private readonly ValvePublisher _valvePublisher;
    
    public IrrigationScheduler(IrrigationAlgorithm algorithm, ValvePublisher valvePublisher)
    {
        _algorithm = algorithm;
        _valvePublisher = valvePublisher;

        HandleTimer();
    }

    private async void HandleTimer()
    {
        using var cts = new CancellationTokenSource();
        using var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await periodicTimer.WaitForNextTickAsync(cts.Token))
        {
            int minutesToIrrigate = _algorithm.GetMinutesToIrrigate();
            if (minutesToIrrigate > 0)
            {
                await _valvePublisher.PublishValveOpen("True");
                continue;
            }
            await _valvePublisher.PublishValveOpen("False");
        }
    }
}