import React from "react";
import { Container } from "react-bootstrap";
import { Route, Routes } from "react-router-dom";
import { TopNavbar } from "../Components/TopNavbar";
import { About } from "./About";
import { Community } from "./Community";
import { GameDetails } from "./GameDetails";
import { Home } from "./Home";
import { NewGame } from "./NewGame";
import { Support } from "./Support";

export function WithNavbar() {
  return (
    <Container>
      <TopNavbar />

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/community" element={<Community />} />
        <Route path="/about" element={<About />} />
        <Route path="/support" element={<Support />} />
        <Route path="/games/:id" element={<GameDetails />} />
        <Route path="/new-game" element={<NewGame />} />
        <Route path="/edit-game/:id" element={<NewGame />} />
      </Routes>
    </Container>
  )
}