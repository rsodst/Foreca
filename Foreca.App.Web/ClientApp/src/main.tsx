import React, {Dispatch, SetStateAction, useState} from "react";
import ReactDOM from "react-dom/client";
import {createGlobalStyle} from "styled-components";
import App from "./components/App";
import CityDetails from "./models/cityDetails";

export type ForecaAppStateType = {
    cityDetails?: CityDetails
};

export const ForecaAppContext = React.createContext<{
    state: ForecaAppStateType,
    setState: Dispatch<SetStateAction<ForecaAppStateType>>
} | null>(null);

function AppStateWrapper() {
    const [appState, setAppState] = useState<ForecaAppStateType>({});

    return <ForecaAppContext.Provider value={{
        state: appState,
        setState: setAppState
    }}>
        <App/>
    </ForecaAppContext.Provider>;
}

const GlobalStyle = createGlobalStyle`
* {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }
`;

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <React.StrictMode>
        <GlobalStyle/>
        <AppStateWrapper/>
    </React.StrictMode>
);
