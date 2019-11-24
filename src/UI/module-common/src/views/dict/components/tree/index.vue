<template>
  <div class="nm-common-dict-tree">
    <div class="top" ref="top">
      <el-input :placeholder="placeholder" :disabled="disabled" :suffix-icon="actived ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" :value="label" readonly @click.native="onInputClick"> </el-input>
    </div>
    <transition name="el-zoom-in-top">
      <div ref="bottom" class="bottom" :style="{ height, width }" v-show="actived">
        <nm-box page header footer :title="title" icon="channel">
          <template v-slot:toolbar>
            <nm-button icon="close" @click="actived = false" />
          </template>
          <el-input v-if="filterable" v-model="keyword" clearable placeholder="请输入名称" />
          <el-tree ref="tree" v-bind="tree" v-on="on" class="nm-tree">
            <span slot-scope="{ data }">
              <nm-icon :name="data.item.icon || 'attachment'" />
              <span class="nm-m-l-5">{{ data.label }}</span>
            </span>
          </el-tree>
          <template v-slot:footer>
            <span style="float:left;" v-if="multipleLimit > 0"
              >最多选择<label class="nm-size-18 nm-text-danger nm-p-3">{{ multipleLimit }}</label
              >个</span
            >
            <nm-button size="small" type="primary" text="确定" @click="onSave" />
            <nm-button size="small" type="info" text="重置" @click="reset" />
          </template>
        </nm-box>
        <div ref="arrow" class="arrow" />
      </div>
    </transition>
  </div>
</template>
<script>
const api = $api.common.dict
export default {
  data() {
    return {
      data: null,
      title: '',
      tree: {
        data: null,
        nodeKey: 'id',
        showCheckbox: true,
        checkStrictly: true,
        props: { children: 'children', label: 'label' },
        defaultExpandedKeys: []
      },
      on: {
        check: this.onCheck,
        'node-expand': this.onNodeExpand,
        'node-collapse': this.onNodeCollapse
      },
      selection: [],
      actived: false,
      keyword: '',
      filterTimer: null
    }
  },
  props: {
    value: [String, Number, Array],
    group: {
      type: String,
      required: true
    },
    code: {
      type: String,
      required: true
    },
    //多选
    multiple: Boolean,
    //多选时用户最多可以选择的项目数，为 0 则不限制
    multipleLimit: {
      type: Number,
      default: 0
    },
    //禁用
    disabled: Boolean,
    filterable: Boolean,
    placeholder: {
      type: String,
      default: '请选择...'
    },
    height: {
      type: String,
      default: '400px'
    },
    width: {
      type: String,
      default: '260px'
    },
    //多选时的分隔符
    separator: {
      type: String,
      default: '/'
    },
    //过滤操作延迟时间
    delay: {
      type: Number,
      default: 600
    }
  },
  computed: {
    label() {
      return this.selection.map(m => m.name).join(this.separator)
    },
    value_() {
      if (this.multiple) {
        return this.selection.map(m => m.id)
      } else {
        if (this.selection.length > 0) {
          return this.selection[0].id
        } else {
          return 0
        }
      }
    },
    selection_() {
      if (this.multiple) {
        return this.selection
      } else {
        if (this.selection.length > 0) {
          return this.selection[0]
        } else {
          return null
        }
      }
    }
  },
  methods: {
    //刷新
    refresh() {
      const params = { group: this.group, code: this.code }
      api.tree(params).then(data => {
        this.data = data.children
        this.tree.data = this.data
        this.title = data.label
        this.onChange()
      })
    },
    //递归获取当前节点
    getItem(list, id) {
      for (let i = 0; i < list.length; i++) {
        const m = list[i]
        if (m.id === id) {
          return m.item
        }

        if (m.children) {
          const item = this.getItem(m.children, id)
          if (item != null) return item
        }
      }
      return null
    },
    setCheckedKeys(keys) {
      this.$refs.tree.setCheckedKeys(keys || this.selection.map(m => m.id))
    },
    onChange() {
      this.selection = []
      let defaultExpandedKeys = []
      if (this.multiple) {
        //多选
        this.value.forEach(id => {
          let item = this.getItem(this.tree.data, id)
          if (item) {
            defaultExpandedKeys = defaultExpandedKeys.concat(item.idList)
            this.selection.push(item)
          }
        })
      } else {
        //单选
        let item = this.getItem(this.tree.data, this.value)
        if (item) {
          defaultExpandedKeys = item.idList
          this.selection = [item]
        }
      }
      this.tree.defaultExpandedKeys = defaultExpandedKeys
      this.$nextTick(() => {
        this.setCheckedKeys()
        this.$emit('change', this.value_, this.selection_)
      })
    },
    onNodeExpand(data) {
      //记录展开的节点
      this.tree.defaultExpandedKeys.push(data.id)
    },
    onNodeCollapse(data) {
      //移除展开的节点
      this.$_.pull(this.tree.defaultExpandedKeys, data.id)
    },
    onInputClick() {
      //禁用
      if (this.disabled) return

      this.actived = !this.actived

      if (this.actived) {
        this.$nextTick(() => {
          let $box = this.$refs.bottom
          let $arrow = this.$refs.arrow
          const boxWidth = $box.offsetWidth
          const { x, y, width, height } = this.$refs.top.getBoundingClientRect()
          let left = x + width / 2 - $box.offsetWidth / 2
          // 判断右侧有没有超出页面
          if (left + boxWidth > document.body.offsetWidth) {
            left = document.body.offsetWidth - boxWidth - 20
            // 计算箭头的位置
            $arrow.style.left = x - left - 10 + width / 2 + 'px'
          } else {
            // 计算箭头的位置
            $arrow.style.left = $box.offsetWidth / 2 + 'px'
          }

          $box.style.left = left + 'px'
          $box.style.top = y + height + 14 + 'px'
        })
      }
    },
    onCheck(data, checked) {
      if (this.multiple) {
        if (checked) {
          if (this.multipleLimit > 0 && this.selection.length === this.multipleLimit) {
            this.setCheckedKeys()
            this._warning(`最多只能选择${this.multipleLimit}个`)
            return
          }
          this.selection.push(data.item)
        } else {
          this.$_.pull(this.selection, data.item)
        }
      } else {
        if (checked) {
          this.selection = [data.item]
        } else {
          this.selection = []
        }
        this.setCheckedKeys([data.id])
      }
    },
    onSave() {
      this.$emit('input', this.value_)
      this.$emit('change', this.value_, this.selection_)
      this.actived = false
    },
    reset() {
      this.selection = []
      this.setCheckedKeys([])
      this.$emit('reset')
    },
    filter(children) {
      if (!children || children.length < 1) return false
      for (var i = children.length - 1; i >= 0; i--) {
        let m = children[i]
        if (!m.label.includes(this.keyword) && !this.filter(m.children)) {
          children.splice(i, 1)
        }
      }
      return children.length > 0
    }
  },
  watch: {
    value(val) {
      if (val !== this.value_) this.onChange()
    },
    keyword(val) {
      if (this.filterable) {
        clearTimeout(this.filterTimer)
        this.filterTimer = setTimeout(() => {
          if (!val) this.tree.data = this.data
          else {
            let data = this.$_.cloneDeep(this.data)
            this.filter(data)
            this.tree.data = data
          }
        }, this.delay)
      }
    }
  },
  mounted() {
    this.refresh()
  }
}
</script>
<style lang="scss">
.nm-common-dict-tree {
  position: relative;
  .top {
    .el-input__inner {
      cursor: pointer;
    }
  }
  .bottom {
    position: fixed;
    background: #fff;
    border-radius: 3px;
    z-index: 9999;

    .arrow {
      position: absolute;
      display: block;
      top: -20px;
      width: 0;
      height: 0;
      border-color: transparent;
      border-style: solid;
      border-top-width: 0;
      border-bottom-color: #dcdfe6;
      border-width: 10px;

      &::before {
        content: '';
        position: absolute;
        top: 1px;
        left: -10px;
        display: block;
        width: 0;
        height: 0;
        border-color: transparent;
        border-style: solid;
        border-width: 10px;
        border-bottom-color: #fff;
        border-top-width: 0;
      }
    }
  }
}
</style>
