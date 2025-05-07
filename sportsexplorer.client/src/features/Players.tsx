import {
  Card,
  CardContent,
  CardMedia,
  FormControl,
  Grid,
  InputLabel,
  LinearProgress,
  MenuItem,
  Select,
  Typography,
  type SelectChangeEvent,
} from "@mui/material";
import { useState } from "react";
import { fetcher } from "../services/api";
import useSWR from "swr";

const seasonId = 719; // 2024/25

interface Player {
  id: number;
  firstName: string;
  lastName: string;
  position: string;
  birthday: string;
  profilePictureUrl: string;
}

interface Team {
  id: number;
  name: string;
}

export default function Players() {
  const [teamId, setTeamId] = useState<string>();

  const { data: teams, error, isLoading } = useSWR<Team[]>(`/api/seasons/${seasonId}/teams`, fetcher);
  const { data: players, isLoading: loadingPlayers } = useSWR<Player[]>(
    `/api/seasons/${seasonId}/teams/${teamId}/players`,
    fetcher
  );

  const handleChange = (event: SelectChangeEvent) => {
    setTeamId(event.target.value);
  };

  return (
    <div>
      <Typography component="h4" variant="h4" align="center" color="text.primary" gutterBottom>
        Premier League 2024/25 season squads
      </Typography>
      <Typography color="text.primary" gutterBottom sx={{ mb: 3 }}>
        Select a team from the list to get started.
      </Typography>
      <FormControl sx={{ mr: 2, width: 200 }} disabled={isLoading}>
        <InputLabel id="team-select-label">Team</InputLabel>
        <Select labelId="team-select-label" id="team-select" value={teamId} label="Team" onChange={handleChange}>
          {teams?.map((t) => (
            <MenuItem key={t.id} value={t.id}>
              {t.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      {(isLoading || loadingPlayers) && <LinearProgress sx={{ m: 2 }} />}
      <Grid container spacing={4} sx={{ mt: 2 }}>
        {players?.map((p) => (
          <Card key={p.id} sx={{ height: "100%", display: "flex", flexDirection: "column", maxWidth: 200 }}>
            <CardMedia
              component="div"
              sx={{ pt: "200px" }}
              image={p.profilePictureUrl}
              title={`${p.firstName} ${p.lastName}`}
            />
            <CardContent sx={{ flexGrow: 1 }}>
              <Typography gutterBottom variant="h5" component="h2">
                {p.firstName} {p.lastName}
              </Typography>
              <Typography variant="caption">{p.position}</Typography>
              <Typography variant="body2" color="text.secondary">
                {p.birthday.substring(0, 10)}
              </Typography>
            </CardContent>
          </Card>
        ))}
      </Grid>
    </div>
  );
}
