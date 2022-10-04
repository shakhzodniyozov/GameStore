import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import Select from "react-select";
import { BASE_URL } from "../constants";
import gameService from "../services/game.service";
import genreService from "../services/genre.service";

export function NewGame() {

  const [genres, setGenres] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedOption, setSelectedOption] = useState();
  const [game, setGame] = useState({ name: "", price: 0, description: "", genreIds: [] });

  const navigate = useNavigate();
  const params = useParams();

  useEffect(() => {
    genreService.getAll().then(result => {
      setGenres(result.data);
      setLoading(false);
    })
      .catch(error => console.log(error));

    if (params.id) {
      gameService.getById(params.id).then(result => {
        setGame(result.data);
        setSelectedOption(result.data.genres.map(g => ({ value: g.id, label: g.parent ? `${g.parent}/${g.name}` : g.name })));
        setGame({ ...result.data, genreIds: [...result.data.genres.map(g => g.id)] });
        setLoading(false);
      });
    }
  }, [params.id]);

  function onSelectChange(value) {
    setSelectedOption(value);
    setGame({ ...game, genreIds: value.map(g => g.value) })
  }

  function addGame() {
    if (!params.id && !game.imageAsBase64) {
      alert("Please upload an photo.");
      return;
    }

    game.name.trim();
    game.description.trim();

    if (params.id) {
      gameService.update(game).then(result => {
        if (result)
          navigate("/");
      });
    }
    else {
      gameService.add(game).then(result => {
        if (result)
          navigate("/");
      });
    }
  }

  function onPhotoUpload(e) {
    const reader = new FileReader();

    reader.onload = (e) => {
      setGame({ ...game, imageAsBase64: e.target.result });
    }

    reader.readAsDataURL(e.target.files[0]);
  }

  return loading ? "Loading..." : (
    <>
      <h2>{params.id ? "Edit game" : "New Game"}</h2>
      <div className="d-flex flex-column align-items-center">
        <div style={{ width: "50%" }}>
          <img src={game.imageAsBase64 ? game.imageAsBase64 : `${BASE_URL}/${game.imagePath}`} alt="" className="image" />
        </div>
        <input type="file" id="game-photo" hidden onChange={e => onPhotoUpload(e)} />
        <Button onClick={_ => document.getElementById("game-photo").click()} className="mt-1">Upload photo</Button>
      </div>
      <Form.Control
        type="text"
        placeholder="Name"
        onChange={e => setGame({ ...game, name: e.target.value })}
        value={game.name}
        className="mt-1"
      />
      <Form.Control
        type="number"
        placeholder="Price"
        onChange={e => setGame({ ...game, price: e.target.value })}
        value={game.price}
        className="mt-1"
      />
      <Form.Control
        type="text"
        as="textarea"
        placeholder="Description"
        onChange={e => setGame({ ...game, description: e.target.value })}
        value={game.description}
        className="mt-1"
      />
      <Select
        options={genres.map(g => {
          return { value: g.id, label: g.parent ? `${g.parent}/${g.name}` : g.name }
        }).sort((a, b) => a.label > b.label ? 1 : a.label < b.label ? -1 : 0)}
        isMulti={true}
        value={selectedOption}
        onChange={onSelectChange}
        className="mt-1"
      />

      <div className="d-grid mt-1">
        <Button onClick={addGame}>{params.id ? "Save" : "Add"}</Button>
      </div>
    </>
  )
}