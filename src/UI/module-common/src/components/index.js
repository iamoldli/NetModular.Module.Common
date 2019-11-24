import AreaSelect from '../views/area/components/select'
import AttachmentImg from '../views/attachment/components/img-preview'
import AttachmentUploadImg from '../views/attachment/components/upload-img'
import AttachmentUploadSingle from '../views/attachment/components/upload-single'
import DictSelect from '../views/dict/components/select'
import DictTree from '../views/dict/components/tree'

export default [
  {
    name: 'nm-area-select',
    component: AreaSelect
  },
  {
    name: 'nm-attachment-img',
    component: AttachmentImg
  },
  {
    name: 'nm-attachment-upload-img',
    component: AttachmentUploadImg
  },
  {
    name: 'nm-attachment-upload-single',
    component: AttachmentUploadSingle
  },
  {
    name: 'nm-dict-select',
    component: DictSelect
  },
  {
    name: 'nm-dict-tree',
    component: DictTree
  }
]
