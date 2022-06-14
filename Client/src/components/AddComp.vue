<template>
  <div class="edit_user">
    <form @submit="addUser">
      <div class="mb-3">
        <label for="exampleInputEmail1" class="form-label">Username</label>
        <input type="text" class="form-control" required v-model="add.tk" />
        <div id="emailHelp" class="form-text">
          We'll never share your email with anyone else.
        </div>
      </div>
      <div class="mb-3">
        <label for="exampleInputPassword1" class="form-label">Password</label>
        <input type="password" class="form-control" required v-model="add.mk" />
      </div>
      <button type="submit" class="btn btn-primary">Submit</button>
    </form>
  </div>
</template>

<script>
import { ref } from "@vue/reactivity";
export default {
  props: ["usersList"],
  setup(props, context) {
    const add = ref({
      tk: "",
      mk: "",
    });
    const addUser = (event) => {
      event.preventDefault();
      const newUser = {
        tk: add.value.tk,
        mk: add.value.mk,
      };
      context.emit("adduser", newUser);
    };
    return {
      add,
      addUser,
    };
  },
  methods: {},
};
</script>

<style>
.edit_user {
  width: 60%;
  margin: 0 auto;
  margin-bottom: 10px;
}
.form-label {
  right: 0;
}
.form-control-name {
  margin-bottom: 30px;
}
</style>
