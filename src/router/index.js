import { createRouter, createWebHistory } from "vue-router";
import LoginView from "../views/LoginView.vue";

const routes = [
  {
    path: "/",
    name: "Login",
    component: LoginView,
  },
  {
    path: "/about",
    name: "about",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/AboutView.vue"),
    // meta: {
    //   requiresUnauth: true,
    // },
  },
  {
    path: "/list",
    name: "list",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "list" */ "../views/ListUser.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

// router.beforeEach((to, from, next) => {
//   if (sessionStorage.getItem("Token") == null) {
//     next("/");
//   } else {
//     next();
//   }
// });

export default router;
