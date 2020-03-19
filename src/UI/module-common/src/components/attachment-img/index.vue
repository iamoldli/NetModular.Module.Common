<template>
  <div :style="{ width, height }" class="nm-attachment-img-preview">
    <img v-if="url" :src="url" />
    <nm-icon class="no-picture" v-else name="photo" />
  </div>
</template>
<script>
// 接口
const api = $api.common.attachment

export default {
  data() {
    return {
      url: ''
    }
  },
  props: {
    id: {
      type: String
    },
    width: {
      type: String,
      default: 'auto'
    },
    height: {
      type: String,
      default: 'auto'
    }
  },
  methods: {
    get() {
      if (this.id) {
        api.preview(this.id).then(url => {
          this.url = url
        })
      } else {
        this.url = ''
      }
    }
  },
  mounted() {
    this.get()
  },
  watch: {
    id() {
      this.get()
    }
  }
}
</script>
<style lang="scss">
.nm-attachment-img-preview {
  display: block;
  img {
    width: 100%;
  }
}
</style>
