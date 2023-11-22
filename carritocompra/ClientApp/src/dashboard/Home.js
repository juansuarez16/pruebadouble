import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext'; // Asegúrate de importar correctamente tu contexto
import { Link } from 'react-router-dom';
export const Home = () => {
  const { isAuthenticated } = useContext(AuthContext);

  return (
    <div>
      <h2>Panel de Control</h2>
      {isAuthenticated ? (
        <p>Bienvenido al panel de control de tu aplicación.</p>
      ) : (
        <div>
          <p>Debes iniciar sesión para acceder al panel de control.</p>
          <Link to="/">
            <button>Iniciar sesión</button>
          </Link>
        </div>
      )}

      {isAuthenticated && (
        <div>
          <h3>Resumen</h3>
          <p>Aquí puedes mostrar información importante o estadísticas clave de tu aplicación.</p>
        </div>
      )}
    </div>
  );
};