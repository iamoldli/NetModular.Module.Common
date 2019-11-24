<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_">
    <nm-split class="page" v-model="split">
      <template v-slot:left>
        <tree ref="tree" :dict="dict" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <index ref="index" :dict="dict" :parent-id="parentId" @add-success="onAdd" @del-success="onDelete" @edit-success="onEdit" />
      </template>
    </nm-split>
  </nm-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import Tree from './components/tree'
import Index from './components/index'
export default {
  mixins: [mixins.dialog],
  components: { Tree, Index },
  data() {
    return {
      dialog: {
        title: '数据项配置',
        icon: 'tag',
        width: '80%',
        height: '80%',
        noScrollbar: true
      },
      split: 0.2,
      parentId: 0
    }
  },
  props: {
    dict: Object
  },
  methods: {
    onTreeChange(selection) {
      this.parentId = selection.id
    },
    onAdd(model) {
      this.$refs.tree.insert(model)
    },
    onDelete(id) {
      this.$refs.tree.remove(id)
    },
    onEdit(model) {
      this.$refs.tree.update(model)
    }
  }
}
</script>
