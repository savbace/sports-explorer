import { Card, CardMedia, CardContent, Typography } from "@mui/material";

interface PlayerCardProps {
  id: number;
  firstName: string;
  lastName: string;
  position: string;
  birthday: string;
  profilePictureUrl: string;
}

export default function PlayerCard({
  id,
  profilePictureUrl,
  firstName,
  lastName,
  birthday,
  position,
}: PlayerCardProps) {
  return (
    <Card key={id} sx={{ height: "100%", display: "flex", flexDirection: "column", maxWidth: 200 }}>
      <CardMedia component="div" sx={{ pt: "200px" }} image={profilePictureUrl} title={`${firstName} ${lastName}`} />
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography gutterBottom variant="h5" component="h2">
          {firstName} {lastName}
        </Typography>
        <Typography variant="caption">{position}</Typography>
        <Typography variant="body2" color="text.secondary">
          {birthday.substring(0, 10)}
        </Typography>
      </CardContent>
    </Card>
  );
}
