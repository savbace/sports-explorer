import { Container, CssBaseline } from "@mui/material";
import { indigo } from "@mui/material/colors";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import AppHeader from "./components/AppHeader";
import Players from "./features/Teams/Players";

const defaultTheme = createTheme({
  palette: {
    primary: indigo,
    secondary: {
      main: "#96000f",
    },
  },
});

function App() {
  return (
    <>
      <ThemeProvider theme={defaultTheme}>
        <CssBaseline />
        <AppHeader />
        <main>
          <Container sx={{ py: 2 }} maxWidth="lg">
            <Players />
          </Container>
        </main>
      </ThemeProvider>
    </>
  );
}

export default App;
