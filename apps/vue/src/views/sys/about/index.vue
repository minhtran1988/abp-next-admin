<template>
  <PageWrapper title="About">
    <template #headerContent>
      <div class="flex justify-between items-center">
        <span class="flex-1">
          <a :href="GITHUB_URL" target="_blank">{{ name }}</a>
          It is based on Vue3.0, Vite, Ant-Design-Vue, TypeScript
          The goal of the backend solution is to provide ready-made out-of-the-box solutions and rich examples for the development of medium and large projects. In principle, it will not limit any code for commercial use.
        </span>
      </div>
    </template>
    <Description @register="infoRegister" class="enter-y" />
    <Description @register="register" class="my-4 enter-y" />
    <Description @register="registerDev" class="enter-y" />
  </PageWrapper>
</template>
<script lang="ts" setup>
  import { h } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { PageWrapper } from '/@/components/Page';
  import { Description, DescItem, useDescription } from '/@/components/Description/index';
  import { GITHUB_URL, SITE_URL, DOC_URL } from '/@/settings/siteSetting';

  const { pkg, lastBuildTime } = __APP_INFO__;

  const { dependencies, devDependencies, name, version } = pkg;

  const schema: DescItem[] = [];
  const devSchema: DescItem[] = [];

  const commonTagRender = (color: string) => (curVal) => h(Tag, { color }, () => curVal);
  const commonLinkRender = (text: string) => (href) => h('a', { href, target: '_blank' }, text);

  const infoSchema: DescItem[] = [
    {
      label: 'version',
      field: 'version',
      render: commonTagRender('blue'),
    },
    {
      label: 'Last compilation time',
      field: 'lastBuildTime',
      render: commonTagRender('blue'),
    },
    {
      label: 'Document address',
      field: 'doc',
      render: commonLinkRender('document address'),
    },
    {
      label: 'Preview address',
      field: 'preview',
      render: commonLinkRender('preview address'),
    },
    {
      label: 'Github',
      field: 'github',
      render: commonLinkRender('Github'),
    },
  ];

  const infoData = {
    version,
    lastBuildTime,
    doc: DOC_URL,
    preview: SITE_URL,
    github: GITHUB_URL,
  };

  Object.keys(dependencies).forEach((key) => {
    schema.push({ field: key, label: key });
  });

  Object.keys(devDependencies).forEach((key) => {
    devSchema.push({ field: key, label: key });
  });

  const [register] = useDescription({
    title: 'Production environment dependencies',
    data: dependencies,
    schema: schema,
    column: 3,
  });

  const [registerDev] = useDescription({
    title: 'Development environment dependencies',
    data: devDependencies,
    schema: devSchema,
    column: 3,
  });

  const [infoRegister] = useDescription({
    title: 'Project information',
    data: infoData,
    schema: infoSchema,
    column: 2,
  });
</script>
