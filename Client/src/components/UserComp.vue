<template>
  <div ref="User" class="list_user">
    <h3 class="list_user_name">{{ userid.username }}</h3>
    <div class="list_user_boxname">
      <span @click="handleClickUpdate"><a>Sửa</a></span>
      <span @click="handleDelete"><a>Xóa</a></span>
    </div>
  </div>
  <div class="edit">
    <Update v-if="showUpdate" :id="userid.id" :username="userid.username" />
  </div>
</template>

<script>
import Update from "./UpdateComp.vue";
export default {
  name: "UserComp",
  props: ["userid"],
  components: {
    Update,
  },
  setup(props, context) {
    const handleDelete = () => {
      context.emit("detele", props.userid.id);
    };
    return {
      handleDelete,
    };
  },
  methods: {
    handleClickUpdate() {
      this.showUpdate = !this.showUpdate;
      console.log(this.userid.id);
    },
  },
  data() {
    return {
      showUpdate: false,
    };
  },
};
</script>
<style>
.list_user {
  display: flex;
  justify-content: space-between;
  width: 95%;
  height: 50px;
  background-color: rgba(0, 0, 0, 0.07);
  border: 1px solid rgba(0, 0, 0, 0.09);
  border-radius: 10px;
  margin: auto;
  margin-bottom: 10px;
}
.list_user:hover {
  background-color: rgba(0, 0, 0, 0.05);
}
.list_user_boxname {
  margin-right: 20px;
  padding-top: 20px;
}
.list_user_boxname a {
  margin-left: 5px;
  cursor: pointer;
  color: rgba(0, 0, 0, 0.5);
}
.list_user_boxname a:hover {
  color: rgba(196, 192, 192, 0.5);
}
.list_user_name {
  margin-left: 20px;
  margin-top: 10px;
}
</style>
