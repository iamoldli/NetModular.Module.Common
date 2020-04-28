<script>
import { mixins } from 'netmodular-ui'
const { tree } = $api.common.dict

export default {
  mixins: [mixins.treeSelect],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    group: {
      type: String,
      required: true
    },
    code: {
      type: String,
      required: true
    },
    collapseAll: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    /**已选中的值 */
    value_() {
      let sl = this.selection
      return this.multiple ? sl.map(m => m.value) : sl.length > 0 ? sl[0].value : ''
    }
  },
  methods: {
    query() {
      return tree({ group: this.group, code: this.code })
    },
    refreshTree() {
      this.loading = true
      this.action().then(data => {
        this.title_ = data.label
        this.treeOptions.data = data.children
        this.change()
        this.loading = false
      })
    },
    /**
     * @description 选项更改处理
     */
    change() {
      if (!this.value) return

      let data = this.treeOptions.data
      if (data.length < 1) return

      let value = this.multiple ? this.value : [this.value]
      let ids = this.getIds(data, value)

      //设置显示文本
      this.setLabel(ids)
      //清除已选项
      this.selection = []
      //要展开的选项
      this.treeOptions.defaultExpandedKeys = ids

      this.$nextTick(() => {
        this.setCheckedKeys(ids)
        this.$emit('change', this.value_, this.selection_)
      })
    },
    getIds(list, value, ids) {
      if (!ids) ids = []
      list.forEach(m => {
        if (value.includes(m.item.value)) {
          ids.push(m.id)
        }

        if (m.children && m.children.length > 0) {
          ids.concat(this.getIds(m.children, value, ids))
        }
      })
      return ids
    },
    /**保存 */
    save() {
      if (this.multiple && this.multipleLimit > 0 && this.selection.length > this.multipleLimit) {
        this.showLimitMsg()
        return
      }
      let sl = this.selection
      this.setLabel(this.multiple ? sl.map(m => m.id) : sl.length > 0 ? [sl[0].id] : [])
      this.$emit('input', this.value_)
      this.$emit('change', this.value_, this.selection_)
      this.visible = false
    }
  }
}
</script>
