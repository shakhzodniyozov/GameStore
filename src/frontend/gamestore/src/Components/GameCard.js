import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import "../css/GameCard.css";

export function GameCard({ game, deleteGame }) {

  return (
    <>
      <div className="game-card d-flex flex-column p-1">
        <Link to={`/games/${game.id}`} className="text-decoration-none">
          <img src={game.imagePath ? "http://localhost:5000/" + game.imagePath : ""} alt={game.name} className="gameCard-image img-fluid" />
          <div className="d-flex">
            <div className="d-flex flex-column">
              <span className="fs-3">${game.price}</span>
              <span>{game.genres.map(g => g.parent ? `${g.parent}/${g.name}` : g.name).join(', ')}</span>
              <span><strong>{game.name}</strong></span>
            </div>
          </div>
        </Link>
        <div className="row gx-1 mb-1">
          <div className="col d-grid">
            <Link to={`edit-game/${game.id}`} className="btn btn-outline-primary btn-sm">Edit</Link>
          </div>
          <div className="col d-grid">
            <Button size="sm" variant="outline-danger" onClick={() => deleteGame(game.id)}>Delete</Button>
          </div>
        </div>
        <div className="d-grid">
          <Button size="sm" onClick={() => alert("asd")}>Buy</Button>
        </div>
      </div>
    </>
  )
}