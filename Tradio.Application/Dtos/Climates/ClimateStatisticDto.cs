namespace Tradio.Application.Dtos.Climates
{
    public class ClimateStatisticDto
    {
        public string UserId { get; set; } = string.Empty;

        public double CurrentTemperature { get; set; }

        public double AvgTemperatureForDay { get; set; }

        public double AvgTemperatureForWeek { get; set; }

        public double AvgTemperatureForMonth { get; set; }

        public double CurrentHumidity { get; set; }

        public double AvgHumidityForDay { get; set; }

        public double AvgHumidityForWeek { get; set; }

        public double AvgHumidityForMonth { get; set; }
    }
}
