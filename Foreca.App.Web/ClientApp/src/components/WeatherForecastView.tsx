import styled from "styled-components";
import {useEffect, useState} from "react";
import Api from "../api/api";
import {ConditionsText} from "../models/ConditionsText";
import {WindDirectionText} from "../models/WinDirectionText";
import {WeatherForecastDetails} from "../models/WeatherForecastDetails";

const WeatherForecastView: React.FC<{ cityId: number }> = ({...props}) => {

    const [weatherDetails, setWeatherDetails] = useState<WeatherForecastDetails[] | null>();

    useEffect(() => {

        new Api().getWeatherForecast(props.cityId)
            .then(details => setWeatherDetails(details));

    }, [props.cityId]);

    return <WeatherDetailsBoxWrapper>
        {WeatherForecastDetails && weatherDetails?.map(details =>
            <WeatherDetailsBox key={details.date}>
                <WeatherDetailsBoxTitle>Погода на {details.date}</WeatherDetailsBoxTitle>
                <WeatherDetailsText>
                    <div>Температура (max): {details.tempMax}</div>
                    <div>Температура (min): {details?.tempMin}</div>
                    <div>Обстановка: {ConditionsText[`${details!.condition!}`]}</div>
                    <div>Влажность: {details?.humidity}%</div>
                    <div>Давление (мм.рт.с): {details?.pressure}</div>
                    <div>Скорость ветра (м/с): {details?.windSpeed}</div>
                    <div>Направление ветра: {WindDirectionText[`${details!.windDirection!}`]}</div>
                </WeatherDetailsText>
            </WeatherDetailsBox>)}
    </WeatherDetailsBoxWrapper>;
}

const WeatherDetailsBoxTitle = styled.div`
    border-top-left-radius:5px;
    border-top-right-radius:5px;
    background-color:white;
    display:flex;
    justify-content:center;
    font-family: "Lucida Console", Monaco, monospace;
font-size: 25px;
letter-spacing: 2px;
word-spacing: 2px;
color: black;
font-weight: normal;
text-decoration: none;
font-style: normal;
font-variant: normal;
text-transform: none;
`;
const WeatherDetailsBox = styled.div`
    margin-top:10px;
    margin-bottom:10px;
    border-radius:10px;
    border: 2px solid white;
    padding:5px;
`;

const WeatherDetailsBoxWrapper = styled.div`
    height:500px;
    overflow-y: scroll;
`;

const WeatherDetailsText = styled.div`
font-family: "Lucida Console", Monaco, monospace;
font-size: 25px;
letter-spacing: 2px;
word-spacing: 2px;
color: white;
font-weight: normal;
text-decoration: none;
font-style: normal;
font-variant: normal;
text-transform: none;

`;

export default WeatherForecastView;