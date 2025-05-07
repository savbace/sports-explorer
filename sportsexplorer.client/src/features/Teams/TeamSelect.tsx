import { FormControl, InputLabel, Select, MenuItem, type SelectChangeEvent } from "@mui/material";

interface TeamSelectProps {
  loading: boolean;
  teamId?: string;
  onChange: (event: SelectChangeEvent) => void;
  teams?: { id: number; name: string }[];
}

export default function TeamSelect({ teamId, loading, teams, onChange }: TeamSelectProps) {
  return (
    <FormControl sx={{ mr: 2, width: 200 }} disabled={loading}>
      <InputLabel id="team-select-label">Team</InputLabel>
      <Select labelId="team-select-label" id="team-select" value={teamId} label="Team" onChange={onChange}>
        {teams?.map((t) => (
          <MenuItem key={t.id} value={t.id}>
            {t.name}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
}
