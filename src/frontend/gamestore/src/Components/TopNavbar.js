import { Container, Nav, Navbar } from "react-bootstrap";

export function TopNavbar() {
  return (
    <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
      <Container>
        <Navbar.Brand href="/">Game Store</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/">Games</Nav.Link>
            <Nav.Link href="/community">Community</Nav.Link>
            <Nav.Link href="/about">About</Nav.Link>
            <Nav.Link href="/support">Support</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}