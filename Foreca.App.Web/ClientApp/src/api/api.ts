import CityDetails from "../models/cityDetails";
import {WeatherDetails} from "../models/weatherDetails";
import {WeatherForecastDetails} from "../models/WeatherForecastDetails";

class WeatherApi {
    async initCity(): Promise<CityDetails> {
        let cityName = await fetch("https://ipinfo.io?token=deb719b04ad348")
            .then(p => p.json())
            .then(p => p.city);

        return await fetch(`http://localhost:4430/Cities?city=${cityName}`, {
            method: `POST`
        })
            .then(p => p.json())
            .then(p => p as CityDetails);
    }

    async addCity(cityName: string): Promise<CityDetails> {
        return await fetch(`http://localhost:4430/Cities?city=${cityName}`, {
            method: 'POST'
        })
            .then(p => p.json())
            .then(p => p as CityDetails);
    }

    async getCitites(name?: string): Promise<CityDetails[]> {
        return await fetch("http://localhost:4430/Cities?city=" + name)
            .then(p => p.json());
    }

    async getWeather(cityId?: number): Promise<WeatherDetails> {
        return await fetch(`http://localhost:4430/Weather/${cityId ?? 0}/current`)
            .then(p => p.json())
            .then(p => p as WeatherDetails);
    }

    async getWeatherForecast(cityId: number): Promise<WeatherForecastDetails[]> {
        return await fetch(`http://localhost:4430/Weather/${cityId}/forecast/3`)
            .then(p => p.json())
            .then(p => p as WeatherForecastDetails[]);
    }


}

export default WeatherApi;