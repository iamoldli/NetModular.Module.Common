<template>
  <el-cascader :props="props"></el-cascader>
</template>

<script>
export default {
  data() {
    return {
      props: {
        lazy: true,
        lazyLoad() {}
      }
    }
  }
}
</script>
