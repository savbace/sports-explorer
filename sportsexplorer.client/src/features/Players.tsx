import { useState } from "react";

async function fetchPlayers() {
  const teamId = 1;
  const seasonId = 719; // 2024/25
  
  const response = await fetch(`/api/teams/${teamId}/seasons/${seasonId}/players`);
  const data = await response.json();

  return data;
}

interface Player {
  id: number;
  firstName: string;
  lastName: string;
  position: string;
  birthday: string;
  profilePictureUrl: string;
}

export default function Players() {
  const [players, setPlayers] = useState<Player[]>([]);

  const populatePlayers = async () => {
    const playersList = await fetchPlayers();
    setPlayers(playersList);
  };

  return (
    <div>
      {players.map((p) => (
        <div key={p.id}>
          <img src={p.profilePictureUrl} alt={`Photo for ${p.firstName} ${p.lastName}`}></img>
          <p>
            {p.firstName} {p.lastName}
          </p>
          <p>{p.birthday}</p>
          <p>{p.position}</p>
        </div>
      ))}

      <button onClick={populatePlayers}>Fetch players</button>
    </div>
  );
}
