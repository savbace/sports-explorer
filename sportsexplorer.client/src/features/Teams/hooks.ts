import useSWR from "swr";
import { fetcher } from "../../services/api";

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

const seasonId = 719; // 2024/25

export function useTeams() {
  const { data, isLoading } = useSWR<Team[]>(`/api/seasons/${seasonId}/teams`, fetcher);

  return {
    teams: data,
    isLoading,
  };
}

export function usePlayers(teamId: string | undefined) {
  const { data, isLoading } = useSWR<Player[]>(
    teamId ? `/api/seasons/${seasonId}/teams/${teamId}/players` : null,
    fetcher
  );

  return {
    players: data,
    isLoading,
  };
}
