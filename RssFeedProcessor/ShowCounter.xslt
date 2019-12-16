<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="podcasts">

      <xsl:copy>
      <xsl:apply-templates select="show" />
    </xsl:copy>

  </xsl:template>

  <xsl:template match="podcasts/show/@sid">
    <xsl:variable name="testvar" select="count(../following-sibling::show)" />
    <xsl:attribute name="sid">
      <xsl:value-of select="$testvar"/>
    </xsl:attribute>
  </xsl:template>




</xsl:stylesheet>
