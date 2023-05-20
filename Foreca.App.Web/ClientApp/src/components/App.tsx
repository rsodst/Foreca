import React, {useContext, useEffect} from "react";
import styled from "styled-components";
import Api from "../api/api";
import WeatherDetailsView from "./WeatherDetailsView";
import CitySelector from "./CitySelector";
import {ForecaAppContext} from "../main";

const ForecaApp: React.FC = () => {

    const ctx = useContext(ForecaAppContext);

    useEffect(() => {
        new Api()
            .initCity()
            .then(cityDetails => {
                ctx?.setState({...ctx?.state, cityDetails: cityDetails})
            });
    }, []);

    return <AppStyledContainer backgroundImage={ctx?.state.cityDetails?.imageUrl}>
        {ctx?.state.cityDetails && <WeatherDetailsView></WeatherDetailsView>}
        <CitySelector/>
    </AppStyledContainer>;
}

const AppStyledContainer = styled.div<{ backgroundImage?: string }>`
    position: fixed;
    display: flex;
    width : 100%;
    height : 100%;
    background-color: #a0d2eb;
    background-image: url(${p => p.backgroundImage}});
    background-size: cover;
`;

export default (ForecaApp);