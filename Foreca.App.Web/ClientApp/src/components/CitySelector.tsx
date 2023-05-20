import styled from "styled-components";
import React, {useContext} from "react";
import AsyncCreatableSelect from "react-select/async-creatable";
import Api from "../api/api";
import {ForecaAppContext} from "../main";
import CityDetails from "../models/cityDetails";

const CitySelector: React.FC = () => {

    const ctx = useContext(ForecaAppContext);

    return <Wrapper>
        <CitySelectorWrapper>
            <AsyncCreatableSelect
                placeholder={"Погода в ..."}
                formatOptionLabel={(data: any) => {
                    return <>{data.name ?? data.label}</>
                }}
                loadOptions={(inputValue, callback) => {
                    new Api().getCitites(inputValue)
                        .then(p => callback(p));
                }}
                onChange={(selectedValue: CityDetails) => {
                    if (selectedValue != null) {
                        ctx?.setState({...ctx?.state, cityDetails: selectedValue})
                    }
                }}
                onCreateOption={(input: any) => {
                    new Api().addCity(input).then(cityDetails => {
                        ctx?.setState({...ctx?.state, cityDetails: cityDetails});
                    });
                }}
                noOptionsMessage={() => null}
                allowCreateWhileLoading={true}
                formatCreateLabel={(value) => `Добавить город ${value}`}
            />
        </CitySelectorWrapper>
    </Wrapper>
}

const CitySelectorWrapper = styled.div`
width:300px;
`

const Wrapper = styled.div`
    display:flex;
    justify-content:center;
    align-items:center;
   
    width:100%;
    height:100%;
`;

export default CitySelector;