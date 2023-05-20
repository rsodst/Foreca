import styled from "styled-components";
import {WeatherDetails} from "../models/weatherDetails";
import React, {useContext, useEffect, useState} from "react";
import Api from "../api/api";
import {ConditionsText} from "../models/ConditionsText";
import {WindDirectionText} from "../models/WinDirectionText";
import WeatherForecastView from "./WeatherForecastView";
import {ForecaAppContext} from "../main";

const WeatherDetailsView: React.FC = () => {

    const [weatherDetails, setWeatherDetails] = useState<WeatherDetails | null>();

    let ctx = useContext(ForecaAppContext);

    useEffect(() => {

        new Api().getWeather(ctx!.state!.cityDetails!.id!)
            .then(details => setWeatherDetails(details));

    }, [ctx!.state!.cityDetails!.id]);

    return <WeatherDetailsWrapper>
        <WeatherDetailsBox>
            <WeatherDetailsBoxTitle>Погода в {ctx?.state?.cityDetails?.name} сейчас</WeatherDetailsBoxTitle>
            {weatherDetails && <WeatherDetailsText>
                <div>Температура: {weatherDetails?.temp}</div>
                <div>Ощущается как: {weatherDetails?.feelsLike}</div>
                <div>Обстановка: {ConditionsText[`${weatherDetails!.condition!}`]}</div>
                <div>Влажность: {weatherDetails?.humidity}%</div>
                <div>Давление (мм.рт.с): {weatherDetails?.pressure}</div>
                <div>Скорость ветра (м/с): {weatherDetails?.windSpeed}</div>
                <div>Направление ветра: {WindDirectionText[`${weatherDetails!.windDirection!}`]}</div>
            </WeatherDetailsText>}
        </WeatherDetailsBox>
        <WeatherDetailsBoxSeparator>Прогноз погоды в {ctx?.state?.cityDetails?.name} на 3
            суток</WeatherDetailsBoxSeparator>
        {<WeatherForecastView cityId={ctx?.state?.cityDetails?.id!}></WeatherForecastView>}
    </WeatherDetailsWrapper>
}

const WeatherDetailsBoxSeparator = styled.div`
    border-radius:5px;
    margin-top:10px;
    margin-bottom:10px;
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
    border-radius:10px;
    border: 2px solid white;
    padding:5px;
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

const WeatherDetailsWrapper = styled.div`
background-color:rgba(255, 255, 255, 0.3);
padding:20px;
`;

export default WeatherDetailsView;