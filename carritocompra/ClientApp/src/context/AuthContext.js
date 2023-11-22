import React, { Component, createContext } from 'react';

const AuthContext = createContext();

class AuthProvider extends Component {
  constructor(props) {
    super(props);

    // Intentamos obtener el estado de autenticación del localStorage
    const isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';

    this.state = {
      isAuthenticated,
    };
  }

  login = () => {
    // Al iniciar sesión, actualizamos el estado y guardamos en localStorage
    this.setState({ isAuthenticated: true });
    localStorage.setItem('isAuthenticated', 'true');
  };

  logout = () => {
    // Al cerrar sesión, actualizamos el estado y eliminamos del localStorage
    this.setState({ isAuthenticated: false });
    localStorage.removeItem('isAuthenticated');
  };

  render() {
    const { isAuthenticated } = this.state;
    const { children } = this.props;

    return (
      <AuthContext.Provider value={{ isAuthenticated, login: this.login, logout: this.logout }}>
        {children}
      </AuthContext.Provider>
    );
  }
}

export { AuthProvider, AuthContext };
