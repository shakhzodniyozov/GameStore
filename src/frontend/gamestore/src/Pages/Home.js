import React from "react";
import { useEffect, useState } from "react";
import { Button, Form, OverlayTrigger, Popover } from "react-bootstrap";
import { Link } from "react-router-dom";
import { GameCard } from "../Components/GameCard";
import genreService from "../services/genre.service";
import gameService from "../services/game.service";
import { BsSearch } from "react-icons/bs";
import "../css/Home.css";

export function Home() {

  const [games, setGames] = useState([]);
  const [loading, setLoading] = useState(true);
  const [genres, setGenres] = useState([]);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [searchValue, setSearchValue] = useState("");

  useEffect(() => {
    gameService.getPagedGames(1).then(result => {
      setGames(result.data);
      setLoading(false);
    });

    genreService.getAllWithDetails().then(result => {
      setGenres(result.data);
    });
  }, []);

  function onFilterChange(e) {
    let genresToFilter = [...selectedGenres];

    if (e.checked) {
      genresToFilter = [...genresToFilter, e.name];
      setSelectedGenres([...selectedGenres, e.name]);
    }
    else {
      genresToFilter = selectedGenres.filter(g => g !== e.name);
      setSelectedGenres(genresToFilter);
    }

    const filter = { genres: genresToFilter, searchValue: searchValue };
    gameService.filter(filter).then(result => {
      if (result)
        setGames(result.data);
    });
  }

  function onSearchBtnClick(e) {
    const filter = { genres: selectedGenres, searchValue: searchValue };

    if (searchValue.length < 3)
      return;

    setLoading(true);
    gameService.filter(filter).then(result => {
      if (result) {
        setGames(result.data);
        setLoading(false);
      }
    })
      .catch(error => console.error(error));
  }

  function deleteGame(id) {
    gameService.delete(id).then(result => {
      if (result)
        setGames(games.filter(x => x.id !== id));
    });
  }

  const popover = (
    <Popover id="popover-basic">
      <Popover.Body>
        <ul className="list">
          {genres.sort((a, b) => a.name > b.name ? 1 : a.name < b.name ? -1 : 0).map(g => {
            if (g.children.length > 0) {
              return (
                <li key={g.id}>
                  <Form.Check
                    type="checkbox"
                    name={`${g.name}`}
                    label={`${g.name}`}
                    onChange={e => onFilterChange(e.target)}
                    key={g.id}
                    checked={selectedGenres.indexOf(g.name) !== -1}
                  />
                  <ul className="list ms-3">
                    {g.children.map(x => {
                      return (
                        <li key={x.id}>
                          <Form.Check
                            type="checkbox"
                            name={`${x.name}`}
                            label={`${x.name}`}
                            onChange={e => onFilterChange(e.target)}
                            key={x.id}
                            checked={selectedGenres.indexOf(x.name) !== -1}
                          />
                        </li>
                      )
                    })}
                  </ul>
                </li>
              )
            } else {
              return (
                <li key={g.id}>
                  <Form.Check
                    type="checkbox"
                    name={`${g.name}`}
                    label={`${g.name}`}
                    onChange={e => onFilterChange(e.target)}
                    key={g.id}
                    checked={selectedGenres.indexOf(g.name) !== -1}
                  />
                </li>
              )
            }
          })}
        </ul>
      </Popover.Body>
    </Popover>
  );

  return loading ? <p>Loading...</p> : (
    <>
      <div className="d-flex justify-content-between align-items-center mt-2">
        <OverlayTrigger overlay={popover} placement="bottom" trigger="click">
          <span style={{ cursor: "pointer", userSelect: "none" }}>+ Add genre</span>
        </OverlayTrigger>
        <div className="d-flex search-holder">
          <input type="text" className="search-input" placeholder="search" value={searchValue} onChange={(e) => setSearchValue(e.target.value)} />
          <Button onClick={onSearchBtnClick} disabled={searchValue.length < 3}><BsSearch size={24} /></Button>
        </div>
      </div>
      <div className="d-flex flex-row-reverse mt-1">
        <Link to="/new-game" className="btn btn-sm btn-success">Add game</Link>
      </div>
      <div className="d-flex flex-row">
        {games.map(g => {
          return (<GameCard game={g} key={g.id} deleteGame={deleteGame} />)
        })}
      </div>
    </>
  )
}