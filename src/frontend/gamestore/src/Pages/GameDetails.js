import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { BASE_URL } from "../constants";
import gameService from "../services/game.service";

export function GameDetails() {
  const [game, setGame] = useState();
  const [loading, setLoading] = useState(true);
  const params = useParams();

  useEffect(() => {
    gameService.getById(params.id).then(result => {
      setGame(result.data);
      setLoading(false);
    });
  }, [params.id]);

  return loading ? <p>Loading...</p> : (
    <div className="d-flex flex-column m-3">
      <div className="d-flex justify-content-center">
        <div style={{ width: "50%" }}>
          <img src={game.imagePath ? `${BASE_URL}/${game.imagePath}` : ""} alt="" className="img-fluid" />
        </div>
      </div>
      <span className="fs-4"><strong>{game.name}</strong></span>
      <div className="d-flex justify-content-between">
        <span className="fs-5">${game.price}</span>
        <Button variant="success">Buy</Button>
      </div>
      <hr />
      <div className="mb-2">
        {game.genres.map(g => {
          return (<span className="badge rounded-pill bg-primary me-1" key={g.id}>
            {g.name}
          </span>)
        })}
      </div>
      <p>{game.description}</p>
    </div>
  )
}