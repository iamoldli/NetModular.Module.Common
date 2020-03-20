<template>
  <div class="nm-common-dict-extend-editor">
    <el-button-group class="nm-common-dict-extend-editor-toolbar">
      <template v-for="tool in tools">
        <component :is="tool" :key="tool" :value="value_" @success="onSuccess"></component>
      </template>
    </el-button-group>
    <el-input type="textarea" :rows="5" v-model="value_" clearable />
  </div>
</template>
<script>
import { mapState } from 'vuex'
export default {
  data() {
    return {
      value_: this.value
    }
  },
  props: {
    value: String
  },
  computed: {
    ...mapState('app/system', { globalComponents: s => s.globalComponents }),
    tools() {
      return this.globalComponents.filter(name => {
        //自定义组件名称为nm-common-dict-item-extend-editor-tool的缩写
        return name.startsWith('nm-ccdieet-')
      })
    }
  },
  methods: {
    onSuccess(text) {
      this.value_ = text
    }
  },
  watch: {
    value(val) {
      if (val !== this.value_) this.value_ = val
    },
    value_(val) {
      this.$emit('input', val)
    }
  }
}
</script>
<style lang="scss">
.nm-common-dict-extend-editor {
  &-toolbar {
    > div {
      float: left;
      > .el-button {
        padding: 5px 10px !important;
      }
    }
  }
}
</style>
