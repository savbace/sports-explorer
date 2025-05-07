import { Grid, LinearProgress, Typography, type SelectChangeEvent } from "@mui/material";
import { useState } from "react";
import { usePlayers, useTeams } from "./hooks";
import PlayerCard from "./PlayerCard";
import TeamSelect from "./TeamSelect";

export default function Players() {
  const [teamId, setTeamId] = useState<string>();

  const { teams, isLoading: loadingTeams } = useTeams();
  const { players, isLoading: loadingPlayers } = usePlayers(teamId);

  return (
    <div>
      <Typography component="h4" variant="h4" align="center" color="text.primary" gutterBottom>
        Premier League 2024/25 season squads
      </Typography>
      <Typography color="text.primary" gutterBottom sx={{ mb: 3 }}>
        Select a team from the list to get started.
      </Typography>
      <TeamSelect
        teams={teams}
        loading={loadingTeams}
        teamId={teamId}
        onChange={(event: SelectChangeEvent) => setTeamId(event.target.value)}
      />
      {(loadingTeams || loadingPlayers) && <LinearProgress sx={{ m: 2 }} />}
      <Grid container spacing={4} sx={{ mt: 2 }}>
        {players?.map((p) => (
          <PlayerCard {...p} />
        ))}
      </Grid>
    </div>
  );
}
