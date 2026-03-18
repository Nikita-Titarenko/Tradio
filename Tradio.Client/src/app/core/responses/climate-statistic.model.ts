export interface ClimateStatisticModel {
  userId: string;
  currentTemperature: number;
  avgTemperatureForDay: number;
  avgTemperatureForWeek: number;
  avgTemperatureForMonth: number;
  currentHumidity: number;
  avgHumidityForDay: number;
  avgHumidityForWeek: number;
  avgHumidityForMonth: number;
}
