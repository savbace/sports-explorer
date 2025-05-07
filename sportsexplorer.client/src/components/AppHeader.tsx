import { AppBar, Toolbar, Typography, Box } from "@mui/material";
import SportsSoccerOutlinedIcon from "@mui/icons-material/SportsSoccerOutlined";

export default function AppHeader() {
  return (
    <AppBar position="static">
      <Toolbar>
        <SportsSoccerOutlinedIcon sx={{ mr: 2 }} />
        <Typography variant="h6" color="inherit" noWrap>
          The Football Explorer
        </Typography>
        <Box sx={{ flexGrow: 1 }}>
          <nav></nav>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
