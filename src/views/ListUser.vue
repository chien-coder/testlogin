<template>
  <!-- @close="handleClick" -->
  <div class="list">
    <User
      v-for="user in users"
      v-bind:key="user.id"
      v-bind:userid="user"
      @detele="deteleUser"
    />
    <Add v-if="showAdd" :usersList="users" @adduser="addUser" />
    <span @click="handleClickAdd"><a class="list_user_boxname">Thêm</a></span>
  </div>
</template>

<script>
import User from "../components/UserComp.vue";
import Add from "../components/AddComp.vue";
import { ref } from "vue";
import axios from "axios";

export default {
  name: "ListUser",
  props: {},
  data() {
    return {
      showAdd: false,
    };
  },
  setup() {
    const users = ref([]);
    const addUser = async (newUser) => {
      const data = {
        username: newUser.tk,
        password: newUser.mk,
      };
      try {
        const res = await axios.post(
          "https://192.168.1.132:9889/user/add-new-user",
          data
        );
        users.value.push(res.data);
        console.log(data);
      } catch (error) {
        alert("tài khoản đã tồn tại");
      }
      // console.log(newUser);
    };
    const deteleUser = async (id) => {
      try {
        await axios.delete(
          `https://192.168.1.132:9889/user/delete-user-by-id/` + id
        );
        users.value = users.value.filter((user) => user.id !== id);
      } catch (error) {
        // alert("khong the xoa tai khoan dang dang nhap");
        console.log(error);
        console.log(id);
      }
    };
    const getAll = async () => {
      try {
        const res = await axios.get(
          "https://192.168.1.132:9889/user/get-all-staff"
        );
        console.log(res.data);
        users.value = res.data;
      } catch (error) {
        console.log("error");
      }
    };
    getAll();
    return {
      users,
      addUser,
      deteleUser,
    };
  },
  components: {
    User,
    Add,
  },
  methods: {
    handleClickAdd() {
      this.showAdd = !this.showAdd;
      console.log(this.users.index);
    },
  },
};
</script>

<style>
.list {
  width: 60%;
  height: 100%;
  margin: 0 auto;
  margin-top: 100px;
  border: 1px solid rgba(0, 0, 0, 0.09);
  border-radius: 10px;
  padding-top: 20px;
  padding-bottom: 10px;
}
.list_user_boxname {
  margin-left: 5px;
  cursor: pointer;
  color: rgba(0, 0, 0, 0.5);
}
</style>
