<template>
  <form @submit.prevent="Signin">
    <label class="form-label" for="user">Username</label>
    <div class="form-outline mb-4">
      <input
        type="text"
        id="user"
        class="form-control"
        v-model="user"
        required
      />
    </div>

    <div class="form-outline mb-4">
      <label class="form-label" for="password">Password</label>
      <input
        type="text"
        id="password"
        class="form-control"
        v-model="pass"
        required
      />
    </div>

    <button class="btn btn-primary btn-block">Sign in</button>
  </form>
</template>

<script>
import axios from "axios";
export default {
  name: "HomeView",
  data() {
    return {
      user: "",
      pass: "",
    };
  },
  methods: {
    Signin() {
      const data = {
        username: this.user,
        password: this.pass,
      };
      axios
        .post("https://192.168.1.132:9889/user/login-admin", data)
        .then(() => {
          // console.log(res.data);
          this.$router.push("/list");
        })
        .catch(() => {
          alert("Login fail");
        });
    },
  },
};
</script>
