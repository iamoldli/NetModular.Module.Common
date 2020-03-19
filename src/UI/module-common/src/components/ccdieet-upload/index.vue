<template>
  <div>
    <nm-button icon="upload" @click="visible = true" />
    <nm-dialog title="上传图片" icon="upload" height="400px" footer :visible.sync="visible">
      <div style="margin:20px;text-algin:center;">
        <nm-attachment-upload-img v-bind="options" @success="onUploadSuccess">
          <template v-slot:text>请选择文件</template>
        </nm-attachment-upload-img>
      </div>
      <template v-slot:footer>
        <nm-button type="success" text="确定" @click="onClick" />
      </template>
    </nm-dialog>
  </div>
</template>
<script>
export default {
  data() {
    return {
      value_: this.value,
      visible: false,
      options: {
        module: 'Common',
        group: 'DictTest',
        maxSize: '500kb',
        width: '100px',
        height: '120px',
        iconSize: '3em'
      },
      url: ''
    }
  },
  props: {
    value: String
  },
  methods: {
    onUploadSuccess(res) {
      this.url = res.data.data.url
    },
    onClick() {
      this.$emit('success', this.url)
      this.visible = false
    }
  },
  watch: {
    value(val) {
      if (val !== this.value_) this.value_ = val
    }
  }
}
</script>
