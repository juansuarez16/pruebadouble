import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./dashboard/Home";
import { Login } from "./login";
import { Registration } from "./registre";
import { UserRegistration } from "./users";

const AppRoutes = [
  {
    path: '/Dashboard',
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    index: true,
    path: '/',
    element: <Login />
  },
  {
    path: '/Registre',
    element: <Registration />
  },
  {
    path: '/Users',
    element: <UserRegistration />
  }
];

export default AppRoutes;
